import {DateFilterOption} from "./DateFilterOption";
import { SortOption } from "./SortOption";
import {PaginationRequest} from "../../shared/models/PaginationRequest";

export class FilterGameRequest {
    NameFragment: string | null = null;
    GenreIds: number[] | null = null;
    PlatformTypeIds: number[] | null = null;
    PublisherIds: number[] | null = null;
    PriceFrom: number | null = null;
    PriceTo: number | null = null;
    DateFilterOption: DateFilterOption = DateFilterOption.None;
    SortOption: SortOption = SortOption.MostViewed;
    Pagination: PaginationRequest = {
        PageNumber: 1,
        PageSize: 10
    };
}

