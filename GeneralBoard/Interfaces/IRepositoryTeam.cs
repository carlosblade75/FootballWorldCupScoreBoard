namespace GeneralBoard.Interfaces
{
    public interface IRepositoryTeam
    {
        public string GetTeamNameByCode(string codeTeam);
        public bool ExistTeam(string codeTeam);
    }
}
