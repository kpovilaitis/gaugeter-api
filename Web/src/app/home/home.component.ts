import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '../_models';
import { UserService } from '../_services';
import { Router } from '@angular/router';

@Component({templateUrl: 'home.component.html'})
export class HomeComponent implements OnInit {
    users: User[] = [];
    user: User = JSON.parse(localStorage.getItem('currentUser')); 

    constructor(
        private userService: UserService,
        private router: Router) { }

    ngOnInit() {
        this.userService.getAll().pipe(first()).subscribe(users => { 
            this.users = users; 
        });
    }

    deleteUser() {
    	this.userService.deleteUser(this.user.id).subscribe();
        this.router.navigate(['/login']);
    }

    updateUser(i: number) {
        localStorage.setItem('updateUser', JSON.stringify(this.users[i]));
        this.router.navigate(['/user-update']);
    }
}