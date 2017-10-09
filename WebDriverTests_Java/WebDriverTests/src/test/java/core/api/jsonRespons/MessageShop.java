package core.api.jsonRespons;

public class MessageShop
{
    private int _id;
    private String public_key;
    private String name;
    private String warehouse;

    public int getId() {
        return _id;
    }
    public void setId(int _id) {
        this._id = _id;
    }
    public String getPublicKey() {
        return public_key;
    }
    public void setPublicKey(String public_key) {
        this.public_key = public_key;
    }
    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public String getWarehouse() {
        return warehouse;
    }
    public void setWarehouse(String warehouse) {
        this.warehouse = warehouse;
    }
}