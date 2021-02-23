export class AddUserMealRequest {
    constructor(
        public categoryId: number,
        public mealId: number,
        public consumedDate: Date
    ) { }
}
