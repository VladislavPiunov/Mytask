import { When, Then } from "@badeball/cypress-cucumber-preprocessor";
import MainPage from "../../e2e/main-page";

When("Я посещаю {string}", (url: string) => {
    MainPage.visitPage(url);
});

Then("Меня переводит на страницу авторизации {string}", (url: string) => {
    cy.url().should("be.equals", url);
});

Then("Я вижу поля для ввода логина и пароля", () => {
    cy.get('#username').should("have.attr", "placeholder", "логин");
    cy.get('#password').should("have.attr", "placeholder", "пароль");
});

When("Ввожу логин и пароль", () => {
    cy.get("#username").type('admin');
    cy.get("#password").type('admin').type("{enter}");
});

Then("Я перехожу на главную страницу {string}", (url: string) => {
    cy.url().should("be.equals", url);
});