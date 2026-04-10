import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  totalClients = 1247;
  activePolicies = 892;
  totalRevenue = 2456789;
  pendingTasks = 23;

  recentActivities = [
    {
      title: 'New Client Registration',
      description: 'John Doe registered for auto insurance',
      time: '2 hours ago',
      type: 'Client'
    },
    {
      title: 'Policy Approved',
      description: 'Home insurance policy #12345 approved',
      time: '4 hours ago',
      type: 'Policy'
    },
    {
      title: 'Payment Received',
      description: 'Premium payment of $1,200 received',
      time: '6 hours ago',
      type: 'Payment'
    },
    {
      title: 'Claim Filed',
      description: 'Auto insurance claim #67890 filed',
      time: '8 hours ago',
      type: 'Claim'
    }
  ];

  recentClients = [
    {
      id: 1,
      name: 'John Smith',
      email: 'john.smith@email.com',
      phone: '+1 (555) 123-4567',
      status: 'Active',
      registrationDate: new Date('2024-01-15')
    },
    {
      id: 2,
      name: 'Sarah Johnson',
      email: 'sarah.j@email.com',
      phone: '+1 (555) 987-6543',
      status: 'Pending',
      registrationDate: new Date('2024-01-14')
    },
    {
      id: 3,
      name: 'Michael Brown',
      email: 'm.brown@email.com',
      phone: '+1 (555) 456-7890',
      status: 'Active',
      registrationDate: new Date('2024-01-13')
    },
    {
      id: 4,
      name: 'Emily Davis',
      email: 'emily.d@email.com',
      phone: '+1 (555) 321-6549',
      status: 'Blocked',
      registrationDate: new Date('2024-01-12')
    }
  ];

  constructor(private router: Router) {}

  viewClient(clientId: number): void {
    this.router.navigate(['/clients', clientId]);
  }
}
