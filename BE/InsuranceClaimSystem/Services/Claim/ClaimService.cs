using AutoMapper;
using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Claim.Request;
using InsuranceClaimSystem.DTOs.Claim.Response;
using InsuranceClaimSystem.Models;
using InsuranceClaimSystem.Repositories;
using InsuranceClaimSystem.Services.User;
using ClaimModel = InsuranceClaimSystem.Models.Claim;

namespace InsuranceClaimSystem.Services.Claim
{
    public class ClaimService : IClaimService
    {
        private readonly IUserService _userService;
        private readonly IClaimRepository _claimRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ClaimService(IUserService userService,
            IClaimRepository claimRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _userService = userService;
            _claimRepository = claimRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ClaimResponse>> CreateClaimAsync(UpSertClaimRequest request)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            ClaimModel claim = new ClaimModel();
            claim.CustomerName = request.CustomerName;
            claim.Amount = request.Amount;
            claim.Description = request.Description;
            claim.Status = ClaimStatus.Pending;
            claim.SubmitDate = DateTime.Now;
            claim.UserId = _userService.GetUserId();

            claim = await _claimRepository.CreateClaimAsync(claim);

            var claimResponse = _mapper.Map<ClaimResponse>(claim);

            res = res.SetIsSuccess(true)
                .SetStatusCode(StatusCodes.Status201Created)
                .SetMessage("Create claim successfully")
                .SetData(claimResponse);

            return res.Build();
        }

        public async Task<ApiResponse<ClaimResponse>> GetClaimByIdAsync(string id)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            var claim = await _claimRepository.GetClaimByIdAsync(Guid.Parse(id));

            if (claim != null)
            {
                res = res.SetIsSuccess(true)
                    .SetStatusCode(StatusCodes.Status200OK)
                    .SetMessage("Get claim by id successfully")
                    .SetData(_mapper.Map<ClaimResponse>(claim));
            }
            else
            {
                res = res.SetStatusCode(StatusCodes.Status400BadRequest)
                    .SetMessage("Claim not found");
            }

            return res.Build();
        }

        public async Task<ApiResponse<IEnumerable<ClaimResponse>>> GetClaimsByStatusAsync(ClaimStatus status)
        {
            var res = new ApiResponse<IEnumerable<ClaimResponse>>.Builder();

            return res.Build();
        }

        public async Task<ApiResponse<ClaimResponse>> UpdateClaimAsync(string id, UpSertClaimRequest request)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            var getClaimResponse = await GetClaimByIdAsync(id);

            // If claim not found, then return
            if (!getClaimResponse.IsSuccess)
            {
                return getClaimResponse;
            }

            // Else
            var claim = await _claimRepository.GetClaimByIdAsync(Guid.Parse(id));
            claim.CustomerName = request.CustomerName;
            claim.Amount = request.Amount;
            claim.Description = request.Description;

            claim = await _claimRepository.UpdateClaimAsync(claim);

            res = res.SetIsSuccess(true)
                .SetStatusCode(StatusCodes.Status200OK)
                .SetMessage("Update claim successfully")
                .SetData(_mapper.Map<ClaimResponse>(claim));

            return res.Build();
        }

        public async Task<ApiResponse<ClaimResponse>> DeleteClaimAsync(string id)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            var getClaimResponse = await GetClaimByIdAsync(id);

            // If claim not found, then return
            if (!getClaimResponse.IsSuccess)
            {
                return getClaimResponse;
            }

            // Else
            var claim = await _claimRepository.GetClaimByIdAsync(Guid.Parse(id));

            await _claimRepository.DeleteClaimAsync(claim);

            res = res.SetIsSuccess(true)
                        .SetStatusCode(StatusCodes.Status200OK)
                        .SetMessage("Delete claim successfully")
                        .SetData(_mapper.Map<ClaimResponse>(claim));

            return res.Build();
        }

        public async Task<ApiResponse<ClaimResponse>> ProcessClaimAsync(string id)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            var getClaimResponse = await GetClaimByIdAsync(id);

            // If claim not found, then return
            if (!getClaimResponse.IsSuccess)
            {
                return getClaimResponse;
            }

            // Else
            var random = new Random();

            var claim = await _claimRepository.GetClaimByIdAsync(Guid.Parse(id));
            claim.Status = (ClaimStatus)random.Next(
                                                    (int)ClaimStatus.Approved,
                                                    (int)ClaimStatus.Rejected + 1
                                               );

            claim = await _claimRepository.UpdateClaimAsync(claim);

            res = res.SetIsSuccess(true)
                .SetStatusCode(StatusCodes.Status200OK)
                .SetMessage("Process claim successfully")
                .SetData(_mapper.Map<ClaimResponse>(claim));

            return res.Build();
        }
    }
}
