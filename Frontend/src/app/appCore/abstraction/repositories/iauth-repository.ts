import { Observable } from "rxjs";
import { HttpResponse } from "@angular/common/http";
import { TokenResponseDto } from "@appCore/dtos/auth/tokenResponseDto";
import { LoginRequestDto } from "@appCore/dtos/auth/loginRequestDto";

export interface IAuthRepository {
    login(loginRequestDto: LoginRequestDto) : Observable<TokenResponseDto>;
    refreshToken() : Observable<TokenResponseDto>;
    logout() : Observable<HttpResponse<Object>>;
    clearSession() : void;
}