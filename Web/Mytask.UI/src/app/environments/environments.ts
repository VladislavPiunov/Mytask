export const environment = {
    keyCloakUrl: 'http://localhost:8484',
    mytaskUrl: 'http://localhost:8083/mytask/api'  
}

export const keycloak = {
    clientId: 'mytask-client',
    clientSecret: 'GbQOu0qgq5aS9blYQsg96PileRe0j2WV',
    realm: 'my_realm'
}

export enum MyTaskApiPaths {
    Board = '/board',
    Stage = '/stage',
    Task = '/task'
}

export enum KeyCloakPaths {
    Token = '/auth/realms/my_realm/protocol/openid-connect/token',
    Users = '/auth/admin/realms/my_realm/users'
}