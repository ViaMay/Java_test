test("Home page", function () {
    var driver = createDriver("http://stylu.co", "1024x768", "chrome");

    driver.findElement(By.cssSelector("div > div > button")).click();
    driver.quit();
});
