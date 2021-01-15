using System.Linq;
using System.Collections.Generic;

namespace GeneralBoard.Repository
{
    public class ListTeam
    {
        private Dictionary<string, string> teams;

        public ListTeam()
        {
            teams = new Dictionary<string, string>();

            Inicialize();
        }

        private void Inicialize()
        {
            teams.Add("MXO", "Mexico");
            teams.Add("CAD", "Canada");
            teams.Add("SPA", "Spain");
            teams.Add("BZL", "Brazil");
            teams.Add("GRD", "Germany");
            teams.Add("FRZ", "France");
            teams.Add("URG", "Uruguay");
            teams.Add("ITA", "Italy");
            teams.Add("ARG", "Argentina");
            teams.Add("AUS", "Australia");

            /*
             * 
             * I just put same teams. We must have all the team in the world cup
             */
        }

        public bool ExistTeam (string codeTeam)
        {
            return teams.Where(team => team.Key == codeTeam).Count() == 1;
        }

        public string GetTeamNameByCode(string codeTeam)
        {
            return teams.Where(team => team.Key == codeTeam).Single().Value;
        }
    }
}
