import { computed, inject, Service, signal } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { finalize, Observable, shareReplay, tap } from 'rxjs';
import { IAuthRepository } from '@/app/appCore/abstraction/repositories/iauth-repository';
import { TokenResponseDto } from '@/app/appCore/dtos/auth/tokenResponseDto';
import { LoginRequestDto } from '@/app/appCore/dtos/auth/loginRequestDto';
import { Router } from '@angular/router';

@Service()
export class AuthService implements IAuthRepository {

    protected readonly baseUrl = 'https://localhost:7105';
    private readonly http = inject(HttpClient);
    private readonly routerService = inject(Router);

    private readonly _accessToken = signal<string | null>(null);
    public AccessToken = this._accessToken.asReadonly();
    public IsAuthenticated = computed(() => this._accessToken() !== null);
    
    public login(loginRequestDto: LoginRequestDto): Observable<TokenResponseDto> {
        const url:string = `${this.baseUrl}/${"api/Auth/LoginAsync"}`;
        return this.http.post<TokenResponseDto>(url, loginRequestDto, { withCredentials: true })
        .pipe(tap({
               next: (res) => this._accessToken.set(res.accessToken),
               error: (err) => console.error(err)
            }));
    }

    private refresh$?: Observable<TokenResponseDto>;
    public refreshToken(): Observable<TokenResponseDto> {
        const url:string = `${this.baseUrl}/${"api/Auth/RefreshAsync"}`;
        //??= : Is refresh null or undefined
        //True : do something
        //False : do nothing
        this.refresh$ ??= this.http.post<TokenResponseDto>(url, {}, { withCredentials: true })
        .pipe(
            tap((res) => this._accessToken.set(res.accessToken)),
            finalize(() => (this.refresh$ = undefined)),
            shareReplay(1)
        );
        return this.refresh$;
    }

    public logout(): Observable<HttpResponse<Object>> {
        const url:string = `${this.baseUrl}/${"api/Auth/LogoutAsync"}`;
        return this.http.post<HttpResponse<Object>>(url, {}, { withCredentials: true })
        .pipe(finalize(() => this.clearSession()));
    }
    
    public clearSession(): void {
        this._accessToken.set(null);
        this.routerService.navigate(['/']);
    }
}

