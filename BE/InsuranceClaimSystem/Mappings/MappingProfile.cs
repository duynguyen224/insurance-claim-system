using AutoMapper;
using InsuranceClaimSystem.DTOs.Claim;
using InsuranceClaimSystem.DTOs.User;
using InsuranceClaimSystem.Models;

namespace InsuranceClaimSystem.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Claim, ClaimResponse>();
            CreateMap<AppUser, UserResponse>();
        }
    }
}
