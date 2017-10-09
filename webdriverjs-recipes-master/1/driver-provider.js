var SeleniumServer = require('selenium-webdriver/remote').SeleniumServer;
var webdriver = require('selenium-webdriver');
var promise = webdriver.promise;
var path = require('path');

var SELENIUM_SERVER_JAR = path.resolve(__dirname, 'selenium-server-standalone-3.0.0-beta2.jar');

function DriverProvider() {
	this._server = null;
	this._driver = null;
}

DriverProvider.prototype = {

	startServer: function () {
		this._server = new SeleniumServer(SELENIUM_SERVER_JAR, {port: 4444});
		return this._server.start();
	},

	stopServer: function () {
		return this._server.stop();
	},

	buildDriver: function () {
		this._driver = new webdriver.Builder()
			.usingServer(this._server.address())
			.forBrowser(webdriver.Browser.FIREFOX)
			.build();
		if (!this._driver) {
			throw new Error('Unable to build driver');
		}
		return promise.fulfilled(this._driver);
	},

	destroyDriver: function () {
		return this._driver ?
			this._driver.quit() :
			promise.rejected(new Error('No driver was found'));
	},

	getDriver: function () {
		return this._driver;
	},

	startUp: function () {
		var flow = promise.controlFlow();
		return promise.all([
			flow.execute(this.startServer.bind(this)),
			flow.execute(this.buildDriver.bind(this))
		]);
	},

	tearDown: function () {
		var flow = promise.controlFlow();
		return promise.all([
			flow.execute(this.destroyDriver.bind(this)),
			flow.execute(this.stopServer.bind(this))
		]);
	}

};

module.exports = exports = DriverProvider;
