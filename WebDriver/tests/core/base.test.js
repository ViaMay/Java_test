load("/tests/core/const.test.js");

beforeTest(function () {
    if (SwitchDriver) {
        var driver = createDriver(URL, "1024x768", "chrome");
    }
    else {
        driver = createGridDriver("http://" +  USERNAME + ":" + AUTOMATE_KEY + "@hub.browserstack.com/wd/hub", {
            desiredCapabilities: {
                browser: "Chrome",
                browser_version: "43.0",
                os: "Windows",
                os_version: "10",
                resolution: "1280x1024",
                "browserstack.debug": "true"
            }
        });
        driver.get(URL);
    }
    session.put("driver", driver);
});

afterTest(function (test) {
    var driver = session.get("driver");
    if (test.isFailed())
    {
        session.report().info("Something wrong on the page").withAttachment("Screenshot.png", takeScreenshot(driver));
    }
    driver.quit();
});