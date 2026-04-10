import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiService } from '../../../../services/api.service';

@Component({
  selector: 'app-business-activity-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './business-activity-list.component.html',
  styleUrl: './business-activity-list.component.scss'
})
export class BusinessActivityListComponent implements OnInit {
  businessActivities: any[] = [];
  isLoading = false;
  error: string | null = null;
  searchTerm = '';
  currentPage = 1;
  pageSize = 10;
  totalActivities = 0;
  showAddForm = false;
  newActivity = {
    name: '',
    description: ''
  };

  constructor(
    private apiService: ApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadBusinessActivities();
  }

  async loadBusinessActivities(): Promise<void> {
    try {
      this.isLoading = true;
      this.error = null;
      
      const response = await this.apiService.getAllBusinessActivities().toPromise();
      this.businessActivities = response?.data || [];
      this.totalActivities = this.businessActivities.length;
      
    } catch (error) {
      console.error('Error loading business activities:', error);
      this.error = 'Failed to load business activities. Please try again.';
    } finally {
      this.isLoading = false;
    }
  }

  searchActivities(): void {
    if (this.searchTerm.trim()) {
      this.businessActivities = this.businessActivities.filter(activity => 
        activity.name?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        activity.description?.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.loadBusinessActivities();
    }
  }

  editActivity(activityId: number): void {
    this.router.navigate(['/master-table/business-activity/edit', activityId]);
  }

  async deleteActivity(activityId: number): Promise<void> {
    if (confirm('Are you sure you want to delete this business activity?')) {
      try {
        this.isLoading = true;
        await this.apiService.deleteBusinessActivity(activityId).toPromise();
        await this.loadBusinessActivities(); // Reload the list
      } catch (error) {
        console.error('Error deleting business activity:', error);
        this.error = 'Failed to delete business activity. Please try again.';
      } finally {
        this.isLoading = false;
      }
    }
  }

  viewActivityDetails(activityId: number): void {
    this.router.navigate(['/master-table/business-activity', activityId]);
  }

  toggleAddForm(): void {
    this.showAddForm = !this.showAddForm;
    if (!this.showAddForm) {
      this.resetNewActivity();
    }
  }

  resetNewActivity(): void {
    this.newActivity = {
      name: '',
      description: ''
    };
  }

  async addNewActivity(): Promise<void> {
    if (!this.newActivity.name.trim()) {
      this.error = 'Activity name is required';
      return;
    }

    try {
      this.isLoading = true;
      this.error = null;
      
      await this.apiService.addBusinessActivity(this.newActivity).toPromise();
      await this.loadBusinessActivities(); // Reload the list
      this.toggleAddForm();
      this.resetNewActivity();
      
    } catch (error) {
      console.error('Error adding business activity:', error);
      this.error = 'Failed to add business activity. Please try again.';
    } finally {
      this.isLoading = false;
    }
  }

  refreshActivities(): void {
    this.searchTerm = '';
    this.loadBusinessActivities();
  }

  getStatusBadgeClass(isActive: boolean): string {
    return isActive ? 'bg-success' : 'bg-secondary';
  }

  get paginatedActivities(): any[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.businessActivities.slice(startIndex, endIndex);
  }

  changePage(page: number): void {
    this.currentPage = page;
  }

  get totalPages(): number {
    return Math.ceil(this.businessActivities.length / this.pageSize);
  }

  getPages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  get activeActivitiesCount(): number {
    return this.businessActivities.filter(act => act.isActive).length;
  }

  get inactiveActivitiesCount(): number {
    return this.businessActivities.filter(act => !act.isActive).length;
  }

  navigateToInsuranceCompanies(): void {
    this.router.navigate(['/master-table/insurance-companies']);
  }
}
