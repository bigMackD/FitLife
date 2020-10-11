export interface UsersResponse{
    success: boolean,
    errors: string[]
    users: User[]
}

export interface User{
    id: string,
    email: string,
    fullName: string
}