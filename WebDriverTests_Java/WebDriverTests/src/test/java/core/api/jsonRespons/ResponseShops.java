package core.api.jsonRespons;

import java.util.List;

public class ResponseShops extends TRespons {

    private List<MessageShop> response;
    public List<MessageShop> getResponse() {
        return response;
    }
    public void setResponse(List<MessageShop> response) {
        this.response = response;
    }
}