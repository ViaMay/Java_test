package core.api.jsonRespons;

import java.util.List;

public class ResponseDeamonPoints extends TRespons {
    private List<MessageOptionsPoints> points;
    public List<MessageOptionsPoints> getPoints() {
        return points;
    }
    public void setPoints(List<MessageOptionsPoints> points) {
        this.points = points;
    }
}