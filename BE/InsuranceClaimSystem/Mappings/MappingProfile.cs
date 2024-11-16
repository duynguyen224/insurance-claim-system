using AutoMapper;
using InsuranceClaimSystem.DTOs.Claim.Response;
using InsuranceClaimSystem.Models;

namespace InsuranceClaimSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Claim, ClaimResponse>();
        }
    }
}
