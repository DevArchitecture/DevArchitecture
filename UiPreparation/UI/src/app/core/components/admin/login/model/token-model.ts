export class TokenModel{
    success:boolean;
    message:string;
    data:Data;
    

}

export class Data {
    expiration:string;
    token:string;
    claims:string[];

}