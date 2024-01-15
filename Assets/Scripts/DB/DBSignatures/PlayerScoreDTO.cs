public class PlayerScoreDTO {
    public string name;

    public int score;

    public PlayerScoreDTO() {
        name = "";
        score = 0;
    }

    public PlayerScoreDTO(string name, int score) {
        this.name = name;
        this.score = score;
    }
}
