using AutoMapper;
using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Factories;
using Barbearia.Domain.Inputs;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Exceptions;

namespace Barbearia.Application.Services
{
	public class BarbeiroService : IBarbeiroService
    {
        private readonly IBarbeiroRepository _barbeiroRepository;
        private readonly BarbeiroFactory _barbeiroFactory;
        private readonly IMapper _mapper;


        public BarbeiroService(IBarbeiroRepository barbeiroRepository, BarbeiroFactory barbeiroFactory, IMapper mapper)
        {
            _barbeiroRepository = barbeiroRepository;
            _barbeiroFactory = barbeiroFactory;
            _mapper = mapper;
        }

        public BarbeiroResponse AdicionarBarbeiro(Guid tenantId, BarbeiroRequest request)
        {
            // validacao de unicidade
            if (_barbeiroRepository.ExisteComMesmoNome(tenantId, request.Nome))
                throw new BusinessException("Já existe um barbeiro com esse nome para o mesmo tenant.");

            var input = _mapper.Map<BarbeiroInput>(request);
            input.SetTenantId(new TenantId(tenantId));
            var barbeiro = _barbeiroFactory.CriarBarbeiro(input);

            _barbeiroRepository.Adicionar(barbeiro);
            _barbeiroRepository.Salvar();

            return _mapper.Map<BarbeiroResponse>(barbeiro);
        }

        public IEnumerable<BarbeiroResponse> ListarBarbeiros(Guid tenantId)
        {
            var barbeiro = _barbeiroRepository.ListarBarbeiros(tenantId)
                .Select(b => new BarbeiroResponse
                {
                    Id = b.Id,
                    Nome = b.Nome
                }).ToList();

            return barbeiro;
        }
    }
}
