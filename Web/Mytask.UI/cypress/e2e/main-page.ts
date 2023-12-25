export default class MainPage {
    static visitPage(url: string) {
        cy.visit(url);
    }

    static submit(ariaLabel: string) {
        cy.get(`input[type='submit'][aria-label='${ariaLabel}']`).click();
    }
}