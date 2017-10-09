var DriverProvider = require('./driver-provider');
var test = require('selenium-webdriver/testing');

test.describe('Page', function () {

	var driverProvider = new DriverProvider();

	test.before(function () {
		return driverProvider.startUp();
	});

	test.after(function () {
		return driverProvider.tearDown();
	});

	test.it('should be valid', function () {
		var driver = driverProvider.getDriver();
		// ...actual tests here
	});

});
