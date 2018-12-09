import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../_models';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User[]>(`${config.apiUrl}/users`);
    }

    deleteUser(id: number) {
        return this.http.delete(`${config.apiUrl}/users/${id}`);
    }

    createUser(user: User) {
        return this.http.post<User>(`${config.apiUrl}/users`, user);
    }

    updateUser(user: User) {
    	return this.http.put<User>(`${config.apiUrl}/users`, user);
    }
}