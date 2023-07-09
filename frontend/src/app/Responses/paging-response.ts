import { PagingData } from "../interfaces/paging-data";

export interface PagingResponse {
    statusCode ?: string,
    isSuccess ?: boolean,
    errorMessages ?: string [],
    pagingData : PagingData [],
    pagingLength : number
}
