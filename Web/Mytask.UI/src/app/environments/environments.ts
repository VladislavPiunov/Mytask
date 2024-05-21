export const environment = {
    keyCloakUrl: 'http://localhost:8484',
    mytaskUrl: 'http://localhost:8083/mytask/api'  
}

export const keycloak = {
    clientId: 'mytask-client',
    clientSecret: '7wj1NxU5PCSC2ZJxtRUIahlHpOGqNVYR',
    realm: 'my_realm'
}

export enum MyTaskApiPaths {
    Board = '/board',
    Stage = '/stage',
    Task = '/task',
    Keycloak = '/keycloak'
}

export enum KeyCloakPaths {
    Token = '/realms/my_realm/protocol/openid-connect/token',
}