namespace EmployeeAPI.model
{
    public class RRF
    {
        public int RRFId { get; set; }
        public int ManagerId { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public System.DateTime SubmissionDate { get; set; }
        public int RoleId { get; set; }
        public int IsBillable { get; set; }
        public decimal BillingRate { get; set; }
        public System.DateTime BillingStartDate { get; set; }
        public int PositionTypeId { get; set; }
        public int IsInternalResource { get; set; }
        public int IdentifiedResourceId { get; set; }
        public int NumberOfPosition { get; set; }
        public int PayroleType { get; set; }
        public int ApprovedByResourceId { get; set; }
        public string PrimaryTechnologies { get; set; }
        public int MinimumYearsOfExperienceId { get; set; }
        public string NiceToHaveSkills { get; set; }
        public string JobLocation { get; set; }
        public int IsRemotely { get; set; }
        public int InterviewByResourceId { get; set; }
        public string JobDescription { get; set; }
        public string OtherInputs { get; set; }

        public string Remark { get; set; }

        public string CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public System.DateTime UpdateDate { get; set; }

    }
}
