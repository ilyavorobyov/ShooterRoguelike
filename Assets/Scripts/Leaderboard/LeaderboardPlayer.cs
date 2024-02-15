public class LeaderboardPlayer
{
    private int _rank;
    private string _name;
    private int _score;

    public LeaderboardPlayer(int rank, string name, int score)
    {
        _rank = rank;
        _name = name;
        _score = score;
    }

    public int Rank => _rank;

    public string Name => _name;

    public int Score => _score;
}