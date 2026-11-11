import { Observable } from "rxjs";
import { HttpResponse } from "@angular/common/http";
import { LoginResponseDto } from "@/app/appCore/dtos/auth/loginResponseDto";
import { LoginRequestDto } from "@appCore/dtos/auth/loginRequestDto";

export interface IAuthRepository {
    login(loginRequestDto: LoginRequestDto) : Observable<LoginResponseDto>;
    refreshToken() : Observable<LoginResponseDto>;
    logout() : Observable<HttpResponse<Object>>;
    clearSession() : void;
    hasAnyRoles<T>(roles: T[]): boolean;
}