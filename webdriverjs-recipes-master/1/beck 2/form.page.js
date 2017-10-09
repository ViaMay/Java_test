var page = require('./page')

var formPage = Object.create(page, {
    /**
     * define elements
     */
    username: { get: function () { return browser.element('#username'); } },

    /**
     * define or overwrite page methods
     */
    open: { value: function() {
        page.open.call(this, 'login');
    } },

    submit: { value: function() {
        this.form.submitForm();
    } }
});

module.exports = formPage
