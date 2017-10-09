const URL = "http://stylu.co";
const USERNAME = "andreiarnautov1";
const AUTOMATE_KEY = "yiFysr9R61GJTRg1TDzt";

this.devices = {
    linux: {
        deviceName: "Linux",
        size: "1024x768",
        osName: "OS X",
        osVersion: "Mavericks",
        //browserName: "Safari",
        browserName: "firefox",
        browserVersion: "7.1"
    },
    windows: {
        deviceName: "Windows",
        size: "1024x768",
        osName: "Windows",
        osVersion: "10",
        browserName: "chrome",
        browserVersion: "43.0"
    }
};

//forAll(devices, function (device) {
//    test("Home page on ${deviceName}", function (device){
//        var driver = createGridDriver("http://" +  USERNAME + ":" + AUTOMATE_KEY + "@hub.browserstack.com/wd/hub", {
//            desiredCapabilities: {
//                browser: device.browserName,
//                browser_version: device.browserVersion,
//                os: device.osName,
//                os_version: device.osVersion,
//                resolution: "1280x1024",
//                "browserstack.debug": "true"
//            }
//        });
//        driver.get(URL);
//    });
//});

forAll(devices, function (device) {
    test("Home page on ${browserName}", function (device){
        var driver = createDriver("http://stylu.co", "1024x768", device.browserName);
        driver.get(URL);
    });
});
