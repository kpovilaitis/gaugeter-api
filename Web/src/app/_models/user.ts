export class User {
    id: number;
    username: string;
    password: string;
    description: string;
    token: string;

    constructor(username: string, password: string, description: string) {
    	this.username = username;
    	this.password = password;
    	this.description = description;
	}
}