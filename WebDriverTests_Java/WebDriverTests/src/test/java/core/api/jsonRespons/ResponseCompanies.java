package core.api.jsonRespons;

public class ResponseCompanies extends TRespons {
    private MessageCompaniesOrShops response;
    public MessageCompaniesOrShops getResponse() {
        return response;
    }
    public void setResponse(MessageCompaniesOrShops response) {
        this.response = response;
    }
}