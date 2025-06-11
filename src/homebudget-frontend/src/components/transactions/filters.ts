import {InjectionKey, Ref} from "vue";
import Transaction from "@/entities/Transaction";
import TransactionForCreation from "@/models/TransactionForCreation";

export const FilterSymbol: InjectionKey<FilterContext> = Symbol('FilterContext');

export interface FilterContext {
    filters: Ref<Filters>;
    setFilters: (update: Partial<Filters>) => void;
    transactions: Ref<Transaction[]>;
    loading: Ref<boolean>;
    addTransaction: (newTransaction: TransactionForCreation) => Promise<void>;
    deleteTransaction: (id: number) => Promise<void>;
}

export interface Filters {
    month: number;
    year: number;
}
