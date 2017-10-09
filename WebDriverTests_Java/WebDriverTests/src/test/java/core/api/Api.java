package core.api;

import com.gargoylesoftware.htmlunit.HttpMethod;
import com.gargoylesoftware.htmlunit.WebClient;
import com.gargoylesoftware.htmlunit.WebRequest;
import com.gargoylesoftware.htmlunit.WebResponse;
import com.gargoylesoftware.htmlunit.util.NameValuePair;
import core.api.jsonRespons.TRespons;
import org.codehaus.jackson.annotate.JsonAutoDetect;
import org.codehaus.jackson.annotate.JsonMethod;
import org.codehaus.jackson.map.DeserializationConfig;
import org.codehaus.jackson.map.ObjectMapper;
import ru.yandex.qatools.allure.annotations.Step;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;


public class  Api {
    private String ApplicationBaseUrl;

    public Api(String value) {
        ApplicationBaseUrl = value;
        mapper = new ObjectMapper().setVisibility(JsonMethod.FIELD, JsonAutoDetect.Visibility.ANY);
        mapper.configure(DeserializationConfig.Feature.FAIL_ON_UNKNOWN_PROPERTIES, false);
    }

    public final Object GET(String date) {
        return GET(date, new ArrayList<NameValuePair>(), TRespons.class);
    }

    public final Object GET(String date, Class classRespons) {
        return GET(date, new ArrayList<NameValuePair>(), classRespons);
    }

    @Step("GET запрос, url {0}, значения {1}")
    public final Object GET(String date, ArrayList<NameValuePair> value, Class classRespons) {
        try (WebClient client = new WebClient()) {
            URL url;
            WebRequest loginRequest;
            try {
                url = new URL("http://" + ApplicationBaseUrl + date);
                loginRequest = new WebRequest(url, HttpMethod.GET);
                loginRequest.setRequestParameters(value);
                WebResponse responseJson = client.loadWebResponse(loginRequest);
                String responseString = responseJson.getContentAsString();
                return ResponsDeserial(ResponseToString(responseString), classRespons);
            } catch (Exception e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    public final Object POST(String date) {
        return POST(date, new ArrayList<NameValuePair>(), TRespons.class);
    }

    public final Object POST(String date, Class classRespons) {
        return POST(date, new ArrayList<NameValuePair>(), classRespons);
    }

    @Step("POST запрос, url {0}, значения {1}")
    public Object POST(String date, ArrayList<NameValuePair> value, Class classRespons) {
        try (WebClient client = new WebClient()) {
            URL url;
            WebRequest loginRequest;
            try {
                url = new URL("http://" + ApplicationBaseUrl + date);
                loginRequest = new WebRequest(url, HttpMethod.POST);
                loginRequest.setRequestParameters(value);
                WebResponse responseJson = client.loadWebResponse(loginRequest);

                String responseString = responseJson.getContentAsString();
                return ResponsDeserial(ResponseToString(responseString), classRespons);
            } catch (Exception e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    @Step("ответ {0}")
    private String ResponseToString (String response)
    {
        return response;
    }

    @Step("преобразуем ответ в {1}")
    public Object ResponsDeserial (String responsString, Class classRespons){
        if (responsString.contains("{\"success\":true,\"response\":")
                && responsString.contains("_id") && responsString.contains("name"))
        {
            String[] split = responsString.split("[{}]", -1);
            String dataString = "{" + split[1] + "[";

            for (int i = 2; i < split.length - 2; i++)
            {
                if (i % 2 != 0)
                {
                    dataString = dataString + "{" + split[i] + "},";
                }
            }
            dataString = dataString + "]}";
            dataString = dataString.replace("},]}", "}]}");
            responsString = dataString;
        }
        try {
            Object responsObject = mapper.readValue(responsString, classRespons);
            return responsObject;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    public ObjectMapper mapper;
}






