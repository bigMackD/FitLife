export class EditProductRequest {
    constructor(public id: number,
        public name: string,
        public proteinsGrams: Number,
        public carbsGrams: Number,
        public fatsGrams: Number) { }
}
