﻿using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface ITeamMemberRepository
    {
        Task<Result<string>> AddBulkAsync(TeamMembersRequest request);
        Task<Result<IEnumerable<TeamMember>>> GetBySchoolIdAsync(int schoolId);
        Task<Result<IEnumerable<TeamMember>>> GetBySportIdAsync(int sportId);
        Task<Result<IEnumerable<TeamMember>>> GetBySchoolAndSportIdAsync(int schoolId, int sportId);
    }
}