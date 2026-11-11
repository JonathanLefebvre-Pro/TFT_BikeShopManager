export interface RefreshResponseDto {
    accessToken: string,
    accessExpireAt: Date,
    userId: number,
    role: string
}