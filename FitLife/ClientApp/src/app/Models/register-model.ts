export class RegisterModel{
    constructor(
        public name: string,
        public surname: string,
        public password: string,
        public email: string,
        public isMale: boolean
    ){}
}