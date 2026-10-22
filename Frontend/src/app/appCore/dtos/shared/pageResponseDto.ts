export interface PageResponseDto<T> {
    data: T[],
    pageNumber: number,
    pageSize: number,
    totalPages: number,
    totalRecords: number,
    hasNextPage: boolean,
    hasPreviousPage: boolean
}