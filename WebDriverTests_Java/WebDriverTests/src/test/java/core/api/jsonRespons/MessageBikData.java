package core.api.jsonRespons;
public class MessageBikData
{
    private int id;
    private String bik;
    private String name;
    private String ks;

    public int getId() {
        return id;
    }
    public void setId(int id) {
        this.id = id;
    }
    public String getBik() {
        return bik;
    }
    public void setBik(String bik) {
        this.bik = bik;
    }
    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public String getKs() {
        return ks;
    }
    public void setKs(String ks) {
        this.ks = ks;
    }
}