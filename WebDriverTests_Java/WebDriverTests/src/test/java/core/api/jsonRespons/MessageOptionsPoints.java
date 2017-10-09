package core.api.jsonRespons;

public class MessageOptionsPoints
{
    private int _id;
    private String name;
    private String city_id;
    private String city;
    private String region_id;
    private String region;
    private String city_type;
    private String area;
    private String kladr;
    private String company;
    private String company_id;
    private String company_code;
    private String metro;
    private String postal_code;
    private String description_in;
    private String description_out;
    private String indoor_place;
    private String address;
    private String schedule;
    private String longitude;
    private String latitude;
    private String has_fitting_room;
    private String is_cash;
    private String is_card;
    private String type;
    private String status;

    public int getId() {
        return _id;
    }
    public void setId(int _id) {
        this._id = _id;
    }
    public String getName() {
    return name;
}
    public void setName(String name) {
        this.name = name;
    }
    public String city_id() {
        return city_id;
    }
    public void city_id(String city_id) {
        this.city_id = city_id;
    }
    public String city() {
        return city;
    }
    public void city(String city) {
        this.city = city;
    }
}