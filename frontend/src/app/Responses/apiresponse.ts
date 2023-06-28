export interface APIResponse {
    statusCode : string, 
    isSuccess : boolean, 
    errorMessages : string [],
    result : {} | string,
    jwtToken : string,
    expirationDate : Date  
}
