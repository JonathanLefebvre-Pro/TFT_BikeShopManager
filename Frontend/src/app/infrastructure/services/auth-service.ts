import { computed, inject, Service, signal } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { finalize, Observable, shareReplay, tap } from 'rxjs';
import { IAuthRepository } from '@/app/appCore/abstraction/repositories/iauth-repository';
import { LoginResponseDto } from '@/app/appCore/dtos/auth/loginResponseDto';
import { RefreshResponseDto } from '@/app/appCore/dtos/auth/refreshResponseDto';
import { LoginRequestDto } from '@/app/appCore/dtos/auth/loginRequestDto';
import { Router } from '@angular/router';

@Service()
export class AuthService implements IAuthRepository {

    // protected readonly baseUrl = 'https://localhost:7105';
    private readonly http = inject(HttpClient);
    private readonly routerService = inject(Router);

    private readonly _accessToken = signal<string | null>(null);
    public AccessToken = this._accessToken.asReadonly();
    public IsAuthenticated = computed(() => this._accessToken() !== null);
    private readonly _role = signal<string | null>(null);
    public Role = this._role.asReadonly();
    private readonly _authUserId = signal<number | null>(null);
    public AuthUserId = this._authUserId.asReadonly();
    
    public login(loginRequestDto: LoginRequestDto): Observable<LoginResponseDto> {
        const url:string = `/${"api/Auth/LoginAsync"}`;
        return this.http.post<LoginResponseDto>(url, loginRequestDto, { withCredentials: true })
        .pipe(tap({
               next: (res) => {
                this._accessToken.set(res.accessToken);
                this._role.set(res.role);
                this._authUserId.set(res.userId);
            },
               error: (err) => console.error(err)
            }));
    }

    private refresh$?: Observable<RefreshResponseDto>;
    public refreshToken(): Observable<RefreshResponseDto> {
        const url:string = `/${"api/Auth/RefreshAsync"}`;
        //??= : Is refresh null or undefined
        //True : do something
        //False : do nothing
        this.refresh$ ??= this.http.post<RefreshResponseDto>(url, {}, { withCredentials: true })
        .pipe(
            tap((res) => {
                this._accessToken.set(res.accessToken);
                this._role.set(res.role);
                this._authUserId.set(res.userId);
            }),
            finalize(() => (this.refresh$ = undefined)),
            shareReplay(1)
        );
        return this.refresh$;
    }

    public logout(): Observable<HttpResponse<Object>> {
        const url:string = `/${"api/Auth/LogoutAsync"}`;
        return this.http.post<HttpResponse<Object>>(url, {}, { withCredentials: true })
        .pipe(finalize(() => this.clearSession()));
    }
    
    public clearSession(): void {
        this._accessToken.set(null);
        this.routerService.navigate(['/']);
    }

    public hasAnyRoles<T>(roles: T[]): boolean {
        return roles.includes(this._role() as T)
    }
}

