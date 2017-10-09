load("/tests/core/default.page.js");
load("/tests/core/signInSignUp.page.js");
load("/tests/core/signInEmail.page.js");
load("/tests/core/base.test.js");

test("Home page", function () {
    var driver = session.get("driver");
    var defaultPage = new DefaultPage(driver);
    defaultPage.signInButton.click();
    var signInSignUpPage = new SignInSignUpPage(driver);
    signInSignUpPage.waitForIt();
    signInSignUpPage.signInEmailLink.click();
    var signInEmailPage = new SignInEmailPage(driver);

    this.report.info("Checked something");
    signInEmailPage.waitForIt();
    signInEmailPage.LoginInput.typeText(userNameAndPass);
    signInEmailPage.PasswordInput.typeText(userNameAndPass);

    var textEmail = signInEmailPage.LoginInput.getText();
    signInEmailPage.SignInButton.click();
});


