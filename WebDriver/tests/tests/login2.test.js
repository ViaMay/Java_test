test("Home page", function () {
    driver = createGridDriver("http://" +  "andreiarnautov1" + ":" + "yiFysr9R61GJTRg1TDzt" + "@hub.browserstack.com/wd/hub", {
        desiredCapabilities: {
            browser: "Chrome",
            browser_version: "43.0",
            os: "Windows",
            os_version: "10",
            resolution: "1280x1024",
            "browserstack.debug": "true"
        }
    });
    driver.get("http://stylu.co");
    driver.findElement(By.cssSelector("div > div > button")).click();
    driver.quit();
});
