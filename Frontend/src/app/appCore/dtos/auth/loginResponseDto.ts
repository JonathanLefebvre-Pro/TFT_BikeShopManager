export interface LoginResponseDto {
    accessToken: string,
    accessExpireAt: Date,
    userId: number,
    role: string
}