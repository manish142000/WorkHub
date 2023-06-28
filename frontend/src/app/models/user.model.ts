export class User {
    constructor(
        public email : string,
        public jwt_token : string, 
        public token_expiration : Date 
    ) {
        
    }

    get token(){
        if( this.jwt_token == null || this.token_expiration < new Date() ) return null;

        return this.jwt_token;
    }
}
