export class AddProductRequest{
    constructor(public name: string,
                public proteinsGrams: Number,
                public carbsGrams: Number,
                public fatsGrams: Number) { }
}
