export interface IUser {
    id:                         string;
    createdTimestamp:           number;
    username:                   string;
    enabled:                    boolean;
    totp:                       boolean;
    emailVerified:              boolean;
    disableableCredentialTypes: any[];
    requiredActions:            any[];
    notBefore:                  number;
    access:                     IAccess;
}

export class User implements IUser {
    constructor (
        public id: string,
        public createdTimestamp: number,
        public username: string,
        public enabled: boolean,
        public totp: boolean,
        public emailVerified: boolean,
        public disableableCredentialTypes: any[],
        public requiredActions: any[],
        public notBefore: number,
        public access: Access
    ) { }
}

export interface IAccess {
    manageGroupMembership: boolean;
    view:                  boolean;
    mapRoles:              boolean;
    impersonate:           boolean;
    manage:                boolean;
}

export class Access implements IAccess {
    constructor (
        public manageGroupMembership: boolean,
        public view: boolean,
        public mapRoles: boolean,
        public impersonate: boolean,
        public manage: boolean
    ){ }

}
