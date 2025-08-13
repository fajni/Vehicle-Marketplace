export class UserAccount {
  
  id!: number;
  firstname!: string;
  lastname!: string;
  email!: string;
  password!: string;
  role!: string;

  constructor(id:number, firstname: string, lastname: string, email: string, password: string, role: string) {
    this.id = id;
    this.firstname = firstname;
    this.lastname = lastname;
    this.email = email;
    this.password = password;
    this.role = role;
  }

  format() {
    return `User: ${this.firstname} ${this.lastname}, 
      Email: ${this.email}, 
      Password: ${this.password}, 
      Role: ${this.role}`;
  }
}