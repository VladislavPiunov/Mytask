import { When, Then } from "@badeball/cypress-cucumber-preprocessor";
import MainPage from "../../e2e/main-page";

const loginPage = "http://localhost:4200/login";

When("Я успешно прохожу авторизацию", () => {
    MainPage.visitPage(loginPage);
    cy.get("#username").type('admin');
    cy.get("#password").type('admin').type("{enter}");
});

Then("Я вижу название доски {string}", (boardName: string) => {
    cy.contains("#project", boardName);
});

Then("Я вижу {int} стадии", (count: number) => {
    cy.get('.board').find('.stage-plate').its('length').should('eq', count);
});

Then("Цвет фона первой стадии {string}", (color: number) => {
    cy.get('.board').find('.stage-plate').first().should('have.css', 'background-color', color);
});