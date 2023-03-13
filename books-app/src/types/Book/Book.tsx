export default interface Book {
  id: number;
  isbn: number;
  name: string;
  authorId: number;
  price: number;
  isDeleted: boolean;
}
