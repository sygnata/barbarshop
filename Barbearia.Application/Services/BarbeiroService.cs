using AutoMapper;
using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Exceptions;

namespace Barbearia.Application.Services
{
	public class BarbeiroService : IBarbeiroService
    {
        private readonly IBarbeiroRepository _barbeiroRepository;
        private readonly IMapper _mapper;


        public BarbeiroService(IBarbeiroRepository barbeiroRepository,  IMapper mapper)
        {
            _barbeiroRepository = barbeiroRepository;
            _mapper = mapper;
        }

        public BarbeiroResponse AdicionarBarbeiro(Guid tenantId, BarbeiroRequest request)
        {
            // validacao de unicidade
            if (_barbeiroRepository.ExisteComMesmoNome(tenantId, request.Nome))
                throw new BusinessException("Já existe um barbeiro com esse nome para o mesmo tenant.");


            var barbeiro = _mapper.Map<Barbeiro>(request);
            barbeiro.TenantId = tenantId;

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
