package core.api.jsonRespons;

public class MessageWarehouse
{
    private int _id;
    private String name;
    private String house;
    private String flat;
    private String postal_code;
    private String city;
    private String street;
    private String contact_person;
    private String contact_phone;
    private String contact_email;
    private String schedule;

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
    public String getHouse() {
        return house;
    }
    public void setHouse(String house) {
        this.house = house;
    }
    public String getFlat() {
        return flat;
    }
    public void setFlat(String flat) {
        this.flat = flat;
    }
    public String getCity() {
        return city;
    }
    public void setCity(String city) {
        this.city = city;
    }
    public String getStreet() {
        return street;
    }
    public void setStreet(String street) {
        this.street = street;
    }
    public String getContactPerson() {
        return contact_person;
    }
    public void SetContactPerson(String postal_code) {
        this.contact_person = contact_person;
    }
    public String getContactPhone() {
        return contact_phone;
    }
    public void setContactPhone(String contact_phone) {
        this.contact_phone = contact_phone;
    }
    public String getContactEmail() {
        return contact_email;
    }
    public void setContactEmail(String postal_code) {
        this.contact_email = contact_email;
    }
    public String getSchedule() {
        return schedule;
    }
    public void SetSchedule(String schedule) {
        this.schedule = schedule;
    }
}